echo "      > [DB] Launch"

# LAUNCH Database Container
if docker_container_exists $DATABASE_CONTAINER
then
    if ! docker_container_running $DATABASE_CONTAINER
    then
        docker_container_start $DATABASE_CONTAINER
    fi
else
    docker_container_create "$DATABASE_IMAGE" "$DATABASE_CONTAINER" "$DATABASE_PORT"

    # TODO
    # UPDATE Database Schema
fi