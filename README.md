# MagamiManager

���K�~�f�o�C�X�V�т̂��߂�ASP.NET Core�A�v���P�[�V�����ł��B

�摜�̕ۑ��ɂ͂�����̃N���E�h�̃I�u�W�F�N�g�X�g���[�W���g�p���܂��B

ASP.NET Core�Ƃ�����̃N���E�h�̎��p��ړI�� https://megami-manager.7474.jp/ �Ńz�X�e�B���O�����s���Ă��܂��B

## Hosting on Ubuntu 16.04 LTS

Microsoft�̃T�|�[�g��Linux�n�ł͌�������Ubuntu�Ƀz�X�e�B���O�����s����B

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

### .NET Core Runtime

https://www.microsoft.com/net/core#linuxubuntu

�A�v���̃o�[�W������1.0�n�Ȃ̂ŁALTS�o�[�W��������ꂽ�B

```sh
$sudo apt-get install dotnet-dev-1.0.0-preview2-003156
```

### Application Restore

#### ����

```sh
$git clone https://github.com/7474/megami-manager.git
$cd megami-manager
$dotnet restore
```

#### ���ʐݒ�

XXX �����Ƃ� appsettings �Ǘ����m�F���� �� Issue�ɋN�[����

�����������āA�`�F�b�N�A�E�g��Ɏ�C�������B

#### ���s

�`���[�g���A���ɏ]���� service �t�@�C�����쐬�����B

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

���ʂ͈ȉ��̃X�j�y�b�g�ŃA�v���P�[�V�������X�V����B

```sh
dotnet restore
sudo systemctl stop kestrel-megami-manager.service
dotnet publish --configuration Release
dotnet ef database update
sudo systemctl start kestrel-megami-manager.service
sudo systemctl status kestrel-megami-manager.service
```

### Entity Framework Core on MariaDB

Entity Framework Core �Œ����w�肵�Ȃ� `string` ��
`MySql.Data.EntityFrameworkCore` �� `varchar(255)` �Ƀ}�b�s���O����B

�ŋ߂�MariaDB�̃f�t�H���g�����R�[�h�� `utf8mb4` �Ȃ̂� `varchar(255)` �̓L�[���ڒ��� 767Byte �����𒴉߂���B
�ꓖ����I����DB�̕����R�[�h�� `utf8` �ō쐬�����B

```sh
$ dotnet ef database update
Project MegamiManager (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
MySql.Data.MySqlClient.MySqlException: Specified key was too long; max key length is 767 bytes
```

�܂� `MySql.Data.EntityFrameworkCore` �������l�����Ɏg�p�����
`update database` �� `__EFMigrationsHistory` �s�݂Ŏ��s���邽�߁A�蓮�ō쐬�����B

Ref: http://stackoverflow.com/questions/40597534/ef-core-table-efmigrationshistory-doesnt-exist

```sql
CREATE DATABASE megamidevice CHARACTER SET utf8;

CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );
```

### Nginx

�����ɏ]����Nginx�Ńv���L�V����B

Ref: https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction

���̃h���C�����z�X�e�B���O���A�����o�[�X�v���L�V������֌W����A
�`���[�g���A���ʂ�̐ݒ肾�� ASP.NET �����̃v���L�V�ݒ肪�S�ʓI�ɔ��f����ƕs�������B

proxy.conf �̓A�v���̃v���L�V�ނ� server �̐ݒ�� include ����悤�ɂ����B

#### HTTPS�Ή�

Let's Encrypt �� certbot-auto �ŏ��������B

�ȉ��̂悤�� root �Ƀt�@�C��������΂������ԋp����悤�ɂ��邱�ƂŁA
certbot �̎��������ŏؖ��������s���ꂽ�B

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

#### ������̃N���E�h �I�u�W�F�N�g�X�g���[�W�̃v���L�V

�����I�ɑ��̃X�g���[�W�Ɉڍs���邱�Ƃ��l�����A
���h���C���Ń��o�[�X�v���L�V�����B

���R�Ȃ���A���N�G�X�g����Host��SetHost����Ă����404�ɂȂ邽�ߒ��ӂ���B

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
