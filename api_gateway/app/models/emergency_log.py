from sqlalchemy import Column, String, JSON, DateTime
import datetime
from app.database import Base

class EmergencyLog(Base):
    __tablename__ = "emergency_logs"
    
    id = Column(String, primary_key=True, index=True)
    action = Column(String, nullable=False)
    user_id = Column(String, nullable=False)
    target_screens = Column(JSON, nullable=False)
    text = Column(String, nullable=False)
    created_at = Column(DateTime, default=datetime.datetime.utcnow, nullable=False)
