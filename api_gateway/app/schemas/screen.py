from typing import List, Optional
from pydantic import BaseModel

class ScreenBase(BaseModel):
    id: str
    name: str
    complex_id: int
    building_id: int
    entrance: Optional[int] = None

class ScreenCreate(ScreenBase):
    pass

class ScreenResponse(ScreenBase):
    current_template_id: Optional[str] = None

    class Config:
        from_attributes = True

class ScreenAssignRequest(BaseModel):
    screen_ids: List[str]
    template_id: str
