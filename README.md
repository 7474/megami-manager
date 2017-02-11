# MagamiManager

���K�~�f�o�C�X�V�т̂��߂�ASP.NET Core�A�v���P�[�V�����ł��B

�摜�̕ۑ��ɂ͂�����̃N���E�h�̃I�u�W�F�N�g�X�g���[�W���g�p���܂��B

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

```sh
$git clone https://github.com/7474/megami-manager.git
$cd megami-manager
$dotnet restore
```

XXX �����Ƃ� appsettings �Ǘ����m�F����

�����������āA�`�F�b�N�A�E�g��Ɏ�C�������B

XXX �����N��������


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

XXX HTTPS

DNS���Z�����Ă�����B

XXX Proxy Image

��ōl����B
