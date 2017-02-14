# MagamiManager

メガミデバイス遊びのためのASP.NET Coreアプリケーションです。

画像の保存にはさくらのクラウドのオブジェクトストレージを使用します。

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

```sh
$git clone https://github.com/7474/megami-manager.git
$cd megami-manager
$dotnet restore
```

XXX 環境ごとの appsettings 管理を確認する

さしあたって、チェックアウト後に手修正した。

XXX 自動起動させる


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

XXX HTTPS

DNSが浸透してからやる。

XXX Proxy Image

後で考える。
