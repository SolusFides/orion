import asyncio
import json
import logging
from typing import Dict, Any, List

logger = logging.getLogger(__name__)

class SSEManager:
    def __init__(self) -> None:
        self._queues: Dict[str, List[asyncio.Queue]] = {}

    def add_connection(self, screen_id: str) -> asyncio.Queue:
        if screen_id not in self._queues:
            self._queues[screen_id] = []
        queue: asyncio.Queue = asyncio.Queue()
        self._queues[screen_id].append(queue)
        logger.info(f"SSE client connected to screen {screen_id}. Total connections: {len(self._queues[screen_id])}")
        return queue

    def remove_connection(self, screen_id: str, queue: asyncio.Queue) -> None:
        if screen_id in self._queues and queue in self._queues[screen_id]:
            self._queues[screen_id].remove(queue)
            logger.info(f"SSE client disconnected from screen {screen_id}.")
            if not self._queues[screen_id]:
                del self._queues[screen_id]

    async def notify(self, screen_id: str, event_type: str, data: Dict[str, Any]) -> None:
        if screen_id in self._queues:
            payload = {"type": event_type, "data": data}
            json_payload = json.dumps(payload)
            for queue in self._queues[screen_id]:
                await queue.put(json_payload)

    async def broadcast_to_all(self, event_type: str, data: Dict[str, Any]) -> None:
        payload = {"type": event_type, "data": data}
        json_payload = json.dumps(payload)
        for screen_queues in self._queues.values():
            for queue in screen_queues:
                await queue.put(json_payload)

    async def notify_multiple(self, screen_ids: List[str], event_type: str, data: Dict[str, Any]) -> None:
        if "all" in screen_ids:
            await self.broadcast_to_all(event_type, data)
        else:
            for sid in screen_ids:
                await self.notify(sid, event_type, data)

sse_manager = SSEManager()
