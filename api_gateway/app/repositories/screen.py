from typing import List, Dict, Any
from sqlalchemy.future import select
from sqlalchemy import update
from app.models.screen import Screen
from app.schemas.screen import ScreenCreate, ScreenAssignRequest
from app.database import session_manager

class ScreenRepository:
    async def get_all(self) -> List[Screen]:
        async with session_manager() as session:
            result = await session.execute(select(Screen))
            return result.scalars().all()
            
    async def get_by_id(self, screen_id: str) -> Screen | None:
        async with session_manager() as session:
            result = await session.execute(select(Screen).filter(Screen.id == screen_id))
            return result.scalars().first()

    async def create(self, data: ScreenCreate) -> Screen:
        async with session_manager() as session:
            db_screen = Screen(
                id=data.id,
                name=data.name,
                complex_id=data.complex_id,
                building_id=data.building_id,
                entrance=data.entrance
            )
            session.add(db_screen)
            await session.commit()
            await session.refresh(db_screen)
            return db_screen
            
    async def update(self, screen_id: str, data: ScreenCreate) -> Screen | None:
        async with session_manager() as session:
            result = await session.execute(select(Screen).filter(Screen.id == screen_id))
            db_screen = result.scalars().first()
            if db_screen:
                db_screen.name = data.name
                db_screen.complex_id = data.complex_id
                db_screen.building_id = data.building_id
                db_screen.entrance = data.entrance
                await session.commit()
                await session.refresh(db_screen)
                return db_screen
            return None

    async def assign_template(self, data: ScreenAssignRequest) -> Dict[str, Any]:
        async with session_manager() as session:
            stmt = (
                update(Screen)
                .where(Screen.id.in_(data.screen_ids))
                .values(current_template_id=data.template_id)
            )
            await session.execute(stmt)
            await session.commit()
            return {"status": "success", "assigned_screens": data.screen_ids}
