echo "      > [DB] Build"

# BUILD Docker Image
cd apps/db/src
docker_image_create "$DATABASE_IMAGE" "."
cd ../../..

# TODO
# BUILD EntityFramework Migration

echo "      > [DB] Done"