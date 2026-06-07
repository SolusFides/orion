from datetime import datetime
from typing import List, Optional
from pydantic import BaseModel

class EmergencyActivateRequest(BaseModel):
    text: str
    screen_ids: List[str]
    priority: int = 1
    timeout_minutes: Optional[int] = None

class EmergencyResetRequest(BaseModel):
    screen_ids: List[str]

class EmergencyResponse(BaseModel):
    id: str
    text: str
    target_screens: List[str]
    priority: int
    timeout_minutes: Optional[int] = None
    activated_by: str
    created_at: datetime
    resolved_at: Optional[datetime] = None

    class Config:
        from_attributes = True

class EmergencyLogResponse(BaseModel):
    id: str
    action: str
    user_id: str
    target_screens: List[str]
    text: str
    created_at: datetime
