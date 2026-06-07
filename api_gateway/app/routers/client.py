import asyncio
from fastapi import APIRouter, HTTPException, Request
from fastapi.responses import StreamingResponse
from app.repositories.screen import ScreenRepository
from app.repositories.template import TemplateRepository
from app.repositories.emergency import EmergencyRepository
from app.services.sse_manager import sse_manager

router = APIRouter(prefix="/api/client", tags=["Client Displays"])
screen_repo = ScreenRepository()
template_repo = TemplateRepository()
emergency_repo = EmergencyRepository()

@router.get("/screen/{screen_id}")
async def get_screen_config(screen_id: str):
    screen = await screen_repo.get_by_id(screen_id)
    if not screen:
        raise HTTPException(status_code=404, detail="Screen not registered")
        
    template_data = None
    if screen.current_template_id:
        template = await template_repo.get_by_id(screen.current_template_id)
        if template:
            template_data = {
                "id": template.id,
                "name": template.name,
                "config": template.config
            }
            
    active_emergencies = await emergency_repo.get_active()
    current_emergency = None
    for em in active_emergencies:
        if "all" in em.target_screens or screen_id in em.target_screens:
            current_emergency = {"text": em.text}
            break
            
    return {
        "screen_id": screen.id,
        "template": template_data,
        "active_emergency": current_emergency
    }

@router.get("/stream/{screen_id}")
async def sse_stream(screen_id: str, request: Request):
    
    screen = await screen_repo.get_by_id(screen_id)
    if not screen:
        raise HTTPException(status_code=404, detail="Screen not registered")

    async def event_generator():
        queue = sse_manager.add_connection(screen_id)
        try:
            while True:
                if await request.is_disconnected():
                    break
                try:
                    data = await asyncio.wait_for(queue.get(), timeout=1.0)
                    yield f"data: {data}\n\n"
                except asyncio.TimeoutError:
                    yield ": ping\n\n"
        finally:
            sse_manager.remove_connection(screen_id, queue)

    return StreamingResponse(event_generator(), media_type="text/event-stream")
