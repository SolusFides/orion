#!/bin/bash
if ! [ -x "$(command -v docker)" ]; then
  echo 'Error: docker is not installed.' >&2
  exit 1
fi

echo "Stopping Orion API Gateway..."
docker compose -f "docker/docker-compose-local.yml" down
echo "Done."
