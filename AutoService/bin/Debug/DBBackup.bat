SET PGPASSWORD=1

cd /D C:\Program Files

cd PostgreSQL

cd 13

cd bin

pg_dump.exe --host 192.168.0.12 --port 5432 --username postgres --format custom --blobs --verbose --file "C:\Users\kruto\OneDrive\Desktop\AllDATA.backup" "AutoService"


