docker_image_create () {
    local key=$1
    local path=$2

    # if ! docker_image_exists $key
    # then
        docker build -t $key $path
    # fi
}

docker_image_exists () {
    local key=$1

    # if [ -z "$(docker images -q)" ]
    # then
    #     return 0
    # fi

    if docker image inspect "$image" >/dev/null 2>&1
    then
        return 1
    fi
    
    return 0
}

docker_image_delete () {
    local key=$1

    docker image rm "$key" >/dev/null
}

docker_container_create () {
    local image=$1
    local container=$2
    # TODO
    #local port=$3
    local port=5432

    docker run --name "$container" -p "$port:$port" -d "$image"
}

docker_container_exists () {
    local key=$1
    
    # if [ -z "$(docker ps -aq)" ]
    # then
    #     return 0
    # fi

    # if [[ -z "$containers" ]]
    # then
    #     return 0
    # fi

    local containers=$(docker ps -a --format '{{.Names}}')
    if "$containers" | grep -qw "$container_name"
    then
        return 0
    fi

    docker inspect "$key" >/dev/null 2>&1
    return $?
}

docker_container_running () {
    local key=$1

    if docker ps --filter "name=$key" --filter "status=running" | grep -w "$key" > /dev/null
    then
        return 0
    else
        return 1
    fi
}

docker_container_stop () {
    local key=$1

    docker container stop "$key" >/dev/null
}

docker_container_start () {
    local key=$1

    docker container start "$key" 
}

docker_container_delete () {
    local key=$1

    docker container rm "$key" >/dev/null
}

docker_container_restart () {
    local key=$1

    docker restart "$key"
}

docker_clean () {
    local CONTAINER=$1
    local IMAGE=$2

    if docker_container_exists $CONTAINER
    then
        if docker_container_running $CONTAINER
        then
            docker_container_stop $CONTAINER
        fi

        docker_container_delete $CONTAINER
    fi

    if docker_image_exists $IMAGE
    then
        docker_image_delete $IMAGE
    fi
}