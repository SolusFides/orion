from app.schemas.screen import ScreenCreate, ScreenAssignRequest

class ScreenRepository:
    async def get_all(self):
        # TODO: Implement DB fetch
        return []

    async def create(self, data: ScreenCreate):
        # TODO: Implement DB insert
        return {"status": "success", "screen_id": data.id}

    async def assign_template(self, data: ScreenAssignRequest):
        # TODO: Implement DB update
        return {"status": "success", "assigned_screens": data.screen_ids}
