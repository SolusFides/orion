from typing import Any, Dict
from pydantic import BaseModel

class TemplateBase(BaseModel):
    name: str
    config: Dict[str, Any]

class TemplateCreate(TemplateBase):
    pass

class TemplateUpdate(TemplateBase):
    pass

class TemplateResponse(TemplateBase):
    id: str

    class Config:
        from_attributes = True
