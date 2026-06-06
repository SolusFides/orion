import uuid
import datetime
from typing import List, Dict, Any
from sqlalchemy.future import select
from app.models.emergency import Emergency
from app.models.emergency_log import EmergencyLog
from app.schemas.emergency import EmergencyActivateRequest, EmergencyResetRequest
from app.database import session_manager

class EmergencyRepository:
    async def activate(self, data: EmergencyActivateRequest, admin_username: str) -> Emergency:
        async with session_manager() as session:
            emergency_id = str(uuid.uuid4())
            db_emergency = Emergency(
                id=emergency_id,
                text=data.text,
                target_screens=data.screen_ids,
                priority=data.priority,
                timeout_minutes=data.timeout_minutes,
                activated_by=admin_username
            )
            session.add(db_emergency)
            
            db_log = EmergencyLog(
                id=str(uuid.uuid4()),
                action="ACTIVATE",
                user_id=admin_username,
                target_screens=data.screen_ids,
                text=data.text
            )
            session.add(db_log)
            
            await session.commit()
            await session.refresh(db_emergency)
            return db_emergency

    async def reset(self, data: EmergencyResetRequest, admin_username: str) -> Dict[str, Any]:
        async with session_manager() as session:
            # Находим все активные ЧС
            result = await session.execute(select(Emergency).filter(Emergency.resolved_at == None))
            active_emergencies = result.scalars().all()
            
            reset_count = 0
            for em in active_emergencies:
                # Если сброс "all", либо есть пересечение экранов ЧС с запросом на сброс
                if "all" in data.screen_ids or "all" in em.target_screens or set(em.target_screens).intersection(set(data.screen_ids)):
                    em.resolved_at = datetime.datetime.utcnow()
                    reset_count += 1
            
            # Записываем лог
            db_log = EmergencyLog(
                id=str(uuid.uuid4()),
                action="RESET",
                user_id=admin_username,
                target_screens=data.screen_ids,
                text="Emergency Reset"
            )
            session.add(db_log)
            
            await session.commit()
            return {"status": "success", "reset_count": reset_count}

    async def get_active(self) -> List[Emergency]:
        async with session_manager() as session:
            result = await session.execute(select(Emergency).filter(Emergency.resolved_at == None))
            return result.scalars().all()


