# MagamiManager

メガミデバイス遊びのためのASP.NET Coreアプリケーションです。

画像の保存にはさくらのクラウドのオブジェクトストレージを使用します。

ASP.NET Coreとさくらのクラウドの試用を目的に https://megami-manager.7474.jp/ でホスティングを試行しています。

## Hosting on Ubuntu 16.04 LTS

MicrosoftのサポートがLinux系では厚そうなUbuntuにホスティングを試行する。

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

### .NET Core Runtime

https://www.microsoft.com/net/core#linuxubuntu

アプリのバージョンが1.0系なので、LTSバージョンを入れた。

```sh
$sudo apt-get install dotnet-dev-1.0.0-preview2-003156
```

### Application Restore

#### 初回

```sh
$git clone https://github.com/7474/megami-manager.git
$cd megami-manager
$dotnet restore
```

#### 環境別設定

XXX 環境ごとの appsettings 管理を確認する → Issueに起票した

さしあたって、チェックアウト後に手修正した。

#### 実行

チュートリアルに従って service ファイルを作成した。

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

当面は以下のスニペットでアプリケーションを更新する。

```sh
dotnet restore
sudo systemctl stop kestrel-megami-manager.service
dotnet publish --configuration Release
dotnet ef database update
sudo systemctl start kestrel-megami-manager.service
sudo systemctl status kestrel-megami-manager.service
```

### Entity Framework Core on MariaDB

Entity Framework Core で長さ指定しない `string` を
`MySql.Data.EntityFrameworkCore` は `varchar(255)` にマッピングする。

最近のMariaDBのデフォルト文字コードは `utf8mb4` なので `varchar(255)` はキー項目長の 767Byte 制限を超過する。
場当たり的だがDBの文字コードは `utf8` で作成した。

```sh
$ dotnet ef database update
Project MegamiManager (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
MySql.Data.MySqlClient.MySqlException: Specified key was too long; max key length is 767 bytes
```

また `MySql.Data.EntityFrameworkCore` を何も考えずに使用すると
`update database` が `__EFMigrationsHistory` 不在で失敗するため、手動で作成した。

Ref: http://stackoverflow.com/questions/40597534/ef-core-table-efmigrationshistory-doesnt-exist

```sql
CREATE DATABASE megamidevice CHARACTER SET utf8;

CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );
```

### Nginx

推奨に従ってNginxでプロキシする。

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

他のドメインもホスティングし、かつリバースプロキシもする関係から、
チュートリアル通りの設定だと ASP.NET 向けのプロキシ設定が全面的に反映さると不具合がある。

proxy.conf はアプリのプロキシむけ server の設定で include するようにした。

#### HTTPS対応

Let's Encrypt の certbot-auto で処理した。

以下のように root にファイルがあればそちらを返却するようにすることで、
certbot の自動処理で証明書が発行された。

```sh
server {
    listen *:80;
    server_name megami-manager.7474.jp
    add_header Strict-Transport-Security max-age=15768000;

    root /var/www/html;

    location /* {
            try_files $uri @redirect;
    }

    location / {
            return 301 https://$host$request_uri;
    }

    location @redirect {
            return 301 https://$host$request_uri;
    }
}
```

#### さくらのクラウド オブジェクトストレージのプロキシ

将来的に他のストレージに移行することも考慮し、
自ドメインでリバースプロキシした。

当然ながら、リクエスト時のHostがSetHostされていると404になるため注意する。

```sh
$ cat megami-image.conf

upstream megamiimage443 {
    server megami-device.b.sakurastorage.jp:443;
}

upstream megamiimage80 {
    server megami-device.b.sakurastorage.jp;
}

server {
    listen *:80;
    server_name megami-image.7474.jp

    root /var/www/html;

    location / {
            try_files $uri @proxy;
    }

    location @proxy {
            proxy_pass http://megamiimage80;
            access_log /var/log/nginx/proxy-access.log;
    }
}

server {
    listen *:443    ssl;
    server_name     megami-image.7474.jp;
    ssl_certificate /etc/letsencrypt/live/megami-image.7474.jp/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/megami-image.7474.jp/privkey.pem;
    ssl_protocols TLSv1.1 TLSv1.2;
    ssl_prefer_server_ciphers on;
    ssl_ciphers "EECDH+AESGCM:EDH+AESGCM:AES256+EECDH:AES256+EDH";
    ssl_ecdh_curve secp384r1;
    ssl_session_cache shared:SSL:10m;
    ssl_session_tickets off;
    ssl_stapling on; #ensure your cert is capable
    ssl_stapling_verify on; #ensure your cert is capable

    add_header Strict-Transport-Security "max-age=63072000; includeSubdomains; preload";
    add_header X-Frame-Options DENY;
    add_header X-Content-Type-Options nosniff;

    root /var/www/html;

    location / {
            try_files $uri @proxy;
    }


    location @proxy {
            proxy_pass  http://megamiimage80;
            access_log /var/log/nginx/proxy-access.log;
    }
}
```
