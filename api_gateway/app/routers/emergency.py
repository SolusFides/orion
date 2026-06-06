from typing import List
from fastapi import APIRouter
from app.schemas.emergency import EmergencyActivateRequest, EmergencyResetRequest, EmergencyResponse, EmergencyLogResponse
from app.repositories.emergency import EmergencyRepository
from app.repositories.emergency_log import EmergencyLogRepository
from app.dependencies import AdminDep

router = APIRouter(prefix="/api/emergency", tags=["Emergency"])
emergency_repo = EmergencyRepository()
log_repo = EmergencyLogRepository()

@router.get("/logs", response_model=List[EmergencyLogResponse])
async def get_emergency_logs(current_user: AdminDep):
    return await log_repo.get_all()

@router.get("/active", response_model=List[EmergencyResponse])
async def get_active_emergencies(current_user: AdminDep):
    return await emergency_repo.get_active()

@router.post("/activate", response_model=EmergencyResponse)
async def activate_emergency(emergency_data: EmergencyActivateRequest, current_user: AdminDep):
    return await emergency_repo.activate(emergency_data, admin_username=current_user.username)

@router.post("/reset")
async def reset_emergency(reset_data: EmergencyResetRequest, current_user: AdminDep):
    return await emergency_repo.reset(reset_data, admin_username=current_user.username)
