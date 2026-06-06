from app.schemas.emergency import EmergencyActivateRequest, EmergencyResetRequest

class EmergencyRepository:
    async def activate(self, data: EmergencyActivateRequest, admin_username: str):
        # TODO: Implement DB insert to emergencies and emergency_logs
        return {"status": "success", "emergency_id": "stub-uuid"}

    async def reset(self, data: EmergencyResetRequest, admin_username: str):
        # TODO: Implement DB update (resolved_at) and insert to emergency_logs
        return {"status": "success"}

    async def get_active(self):
        # TODO: Implement DB fetch
        return []


