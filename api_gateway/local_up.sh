#!/bin/bash
if ! [ -x "$(command -v docker)" ]; then
  echo 'Error: docker is not installed.' >&2
  exit 1
fi

echo "Building and starting Orion API Gateway..."

docker compose -f "docker/docker-compose-local.yml" pull 2>/dev/null || true
docker compose -f "docker/docker-compose-local.yml" up -d --build

echo ""
echo "Local machine is up!"
echo ""
echo "You can check API docs at: http://localhost:8000/docs"
echo ""
echo "Command to down all containers:"
echo "/bin/bash local_down.sh"
echo ""
