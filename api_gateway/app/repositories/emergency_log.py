from typing import List
from sqlalchemy.future import select
from app.models.emergency_log import EmergencyLog
from app.database import session_manager

class EmergencyLogRepository:
    async def get_all(self) -> List[EmergencyLog]:
        async with session_manager() as session:
            result = await session.execute(
                select(EmergencyLog).order_by(EmergencyLog.created_at.desc())
            )
            return result.scalars().all()
