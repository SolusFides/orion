from typing import List
from fastapi import APIRouter, Depends, HTTPException, status
from app.schemas.screen import ScreenCreate, ScreenResponse, ScreenAssignRequest
from app.repositories.screen import ScreenRepository
from app.dependencies import AdminDep

router = APIRouter(prefix="/api/screens", tags=["Screens"])
screen_repo = ScreenRepository()

@router.get("", response_model=List[ScreenResponse])
async def get_screens(current_user: AdminDep):
    return await screen_repo.get_all()

@router.post("", response_model=ScreenResponse)
async def create_screen(screen: ScreenCreate, current_user: AdminDep):
    existing = await screen_repo.get_by_id(screen.id)
    if existing:
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail=f"Screen with id '{screen.id}' already exists. Use PUT to update."
        )
    return await screen_repo.create(screen)

@router.put("/{screen_id}", response_model=ScreenResponse)
async def update_screen(screen_id: str, screen: ScreenCreate, current_user: AdminDep):
    updated_screen = await screen_repo.update(screen_id, screen)
    if not updated_screen:
        raise HTTPException(status_code=404, detail="Screen not found")
    return updated_screen

@router.post("/assign")
async def assign_template(assign_data: ScreenAssignRequest, current_user: AdminDep):
    # TODO: In future, verify if template_id actually exists in DB
    return await screen_repo.assign_template(assign_data)
