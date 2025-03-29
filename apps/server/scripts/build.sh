echo "      > [SERVER] Build"

# CONFIGURE
DIR_SERVER_ARTIFACTS_LOCAL="published"
DIR_SERVER_ARTIFACTS="../../../../ServerArtifacts"

# BUILD
cd apps/server/src/DogeServer/
dir_delete "$DIR_SERVER_ARTIFACTS_LOCAL"
dir_delete "$DIR_SERVER_ARTIFACTS"
dotnet publish -c Release -o published
dir_move "$DIR_SERVER_ARTIFACTS_LOCAL" "$DIR_SERVER_ARTIFACTS"

# TEST
echo "      > [SERVER] Test"
cd ../DogeServer.Tests/
dotnet test /consoleLoggerParameters:ErrorsOnly \
    --logger "console;verbosity=minimal;consoleLoggerParameters=ErrorsOnly;" \
    --collect:"XPlat Code Coverage" \
    --results-directory ../../../../TestResults
cd ../../../../

#TODO: export coverage
#reportgenerator -reports:./TestResults/*/coverage.cobertura.xml -targetdir:./TestResults/CoverageReport -reporttypes:Html

echo "      > [SERVER] End"