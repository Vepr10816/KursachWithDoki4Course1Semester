SET PGPASSWORD=1

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_restore.exe --host 192.168.225.204 --port 5432 --username postgres --dbname "ProbaRestore" --verbose "C:\Users\kruto\source\repos\AutoService\AutoService\BackupFiles\27102022.backup"