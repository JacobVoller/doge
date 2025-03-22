#!/bin/bash

SWAGGER_PATH="src/resources/v2.json"
CLASS_NAME="RegulationClient"
OUT_PATH="src/DogeServer/Clients/$CLASS_NAME.cs"

nswag openapi2csclient \
    /input:"$SWAGGER_PATH" \
    /output:"$OUT_PATH" \
    /classname:"$CLASS_NAME" \
    /namespace:"DogeServer.Clients" \
    /generateClientInterfaces:true \
    /generateDtoTypes:true \
    /generateBaseUrlProperty:true \
    /useHttpClientCreationMethod:true \
    /generateOptionalParameters:true 
#    /generateUpdateJsonSerializerSettingsMethod:S