#!/bin/bash
DOCKERPULL=`docker pull abhishekgautam01/Accounting:latest`
if [[ $DOCKERPULL != *"Status: Image is up to date for"* || $1 == '/f' ]]; then
        echo "Updating"
        docker stop accounting-backend
        docker rm accounting-backend
        docker run -p 8000:80 \
                -e modules__2__properties__ConnectionString=mongodb://172.17.0.1:27017 \
                -d \
                --name accounting-backend \
                abhishekgautam01/accounting:latest
else
        echo "Image is already updated"
fi