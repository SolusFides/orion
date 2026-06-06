from app.models.user import User

class UserRepository:
    async def get_by_username(self, username: str) -> User | None:
        # TODO: Implement DB fetch
        return None
