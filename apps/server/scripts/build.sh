echo "      > [SERVER] Build"

DIR_SERVER_ARTIFACTS_LOCAL="published"
DIR_SERVER_ARTIFACTS="../../../../ServerArtifacts"

# BUILD
cd apps/server/src/DogeServer/
dir_delete "$DIR_SERVER_ARTIFACTS_LOCAL"
dir_delete "$DIR_SERVER_ARTIFACTS"
dotnet publish -c Release -o published
dir_move "$DIR_SERVER_ARTIFACTS_LOCAL" "$DIR_SERVER_ARTIFACTS"
cd ../../../../

echo "      > [SERVER] End"