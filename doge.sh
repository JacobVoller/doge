#!/usr/bin/env bash

set -e # stop script execution on failure

clear
echo "[DOGE] -- START"

DIR_APPS="apps"
DIR_BUILD="build"
PATH_BUILD_ENTRY="$DIR_APPS/$DIR_BUILD/src/entry.sh"

DATABASE_IMAGE="doge-db"
DATABASE_CONTAINER="doge-db-local"
DATABASE_PORT=5432

source "$PATH_BUILD_ENTRY"

echo "[DOGE] -- END"