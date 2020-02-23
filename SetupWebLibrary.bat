@echo off
powershell -command "Expand-Archive SingleFile-master.zip ."
cd SingleFile-master\cli
npm install
