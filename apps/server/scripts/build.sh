#!/bin/bash

SWAGGER_PATH="src/resources/v2.json"
OUT_PATH="src/DogeServer/Clients"

installAutoRest() {
    if ! command -v autorest >/dev/null 2>&1; then
        npm install -g autorest
    fi
}

generateClient() {
    autorest \
        --input-file="$SWAGGER_PATH" \
        --csharp \
        --output-folder="$OUT_PATH" \
        --clear-output-folder=true \
        --generate-metadata=false \
        --use=@autorest/modelerfour \
        --modelerfour.include-readonly-properties=false \
        --csharp.generate-docs=false \
        --csharp.generate-test-project=false \
        --title=EcfrClient \
        --namespace=DogeService \
        --client-side-validation=false \
        --package-name=DogeServer
}

deleteDir() {
    [ -d "$1" ] && rm -rf "$1"
}

installAutoRest
generateClient
deleteDir "src/tests"
deleteDir "$OUT_PATH/Docs"