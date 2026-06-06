from typing import List
from fastapi import APIRouter, Depends, HTTPException, status
from app.schemas.template import TemplateCreate, TemplateUpdate, TemplateResponse
from app.repositories.template import TemplateRepository
from app.dependencies import AdminDep

router = APIRouter(prefix="/api/templates", tags=["Templates"])
template_repo = TemplateRepository()

@router.get("", response_model=List[TemplateResponse])
async def get_templates(current_user: AdminDep):
    return await template_repo.get_all()

@router.post("", response_model=TemplateResponse)
async def create_template(template: TemplateCreate, current_user: AdminDep):
    return await template_repo.create(template)

@router.put("/{template_id}", response_model=TemplateResponse)
async def update_template(template_id: str, template: TemplateUpdate, current_user: AdminDep):
    updated_template = await template_repo.update(template_id, template)
    if not updated_template:
        raise HTTPException(status_code=404, detail="Template not found or deleted")
    return updated_template

@router.patch("/{template_id}/delete")
async def delete_template(template_id: str, current_user: AdminDep):
    success = await template_repo.delete(template_id)
    if not success:
        raise HTTPException(status_code=404, detail="Template not found or already deleted")
    return {"message": f"Template {template_id} soft deleted"}
