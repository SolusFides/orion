from app.schemas.template import TemplateCreate, TemplateUpdate

class TemplateRepository:
    async def get_all(self):
        # TODO: Implement DB fetch
        return []

    async def create(self, data: TemplateCreate):
        # TODO: Implement DB insert
        return {"status": "success", "template_id": "stub-uuid"}

    async def update(self, template_id: str, data: TemplateUpdate):
        # TODO: Implement DB update
        return {"status": "success"}

    async def delete(self, template_id: str):
        # TODO: Implement soft delete
        return {"status": "success", "deleted_id": template_id}
