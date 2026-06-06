from app.models.user import User
from app.database import session_manager
from sqlalchemy.future import select

class UserRepository:
    async def get_by_username(self, username: str) -> User | None:
        async with session_manager() as session:
            result = await session.execute(select(User).filter(User.username == username))
            return result.scalars().first()
