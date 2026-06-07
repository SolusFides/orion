import uuid
from datetime import datetime
from typing import List
from sqlalchemy.future import select
from app.models.template import Template
from app.schemas.template import TemplateCreate, TemplateUpdate
from app.database import session_manager

class TemplateRepository:
    async def get_all(self) -> List[Template]:
        async with session_manager() as session:
            result = await session.execute(select(Template).filter(Template.deleted_at == None))
            return result.scalars().all()
            
    async def get_by_id(self, template_id: str) -> Template | None:
        async with session_manager() as session:
            result = await session.execute(select(Template).filter(Template.id == template_id, Template.deleted_at == None))
            return result.scalars().first()

    async def create(self, data: TemplateCreate) -> Template:
        async with session_manager() as session:
            db_template = Template(
                id=str(uuid.uuid4()),
                name=data.name,
                config=data.config
            )
            session.add(db_template)
            await session.commit()
            await session.refresh(db_template)
            return db_template

    async def update(self, template_id: str, data: TemplateUpdate) -> Template | None:
        async with session_manager() as session:
            result = await session.execute(select(Template).filter(Template.id == template_id, Template.deleted_at == None))
            db_template = result.scalars().first()
            if db_template:
                db_template.name = data.name
                db_template.config = data.config
                await session.commit()
                await session.refresh(db_template)
                return db_template
            return None

    async def delete(self, template_id: str) -> bool:
        async with session_manager() as session:
            result = await session.execute(select(Template).filter(Template.id == template_id, Template.deleted_at == None))
            db_template = result.scalars().first()
            if db_template:
                db_template.deleted_at = datetime.utcnow()
                await session.commit()
                return True
            return False
