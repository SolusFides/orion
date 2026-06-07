from sqlalchemy import Column, String, JSON, Integer, DateTime
import datetime
from app.database import Base

class Emergency(Base):
    __tablename__ = "emergencies"
    
    id = Column(String, primary_key=True, index=True)  # UUID
    text = Column(String, nullable=False)
    target_screens = Column(JSON, nullable=False)  # list of screen IDs, e.g., ["main_hall_01"] or ["all"]
    priority = Column(Integer, default=1, nullable=False)
    timeout_minutes = Column(Integer, nullable=True)
    activated_by = Column(String, nullable=False)  # username of administrator
    created_at = Column(DateTime, default=datetime.datetime.utcnow, nullable=False)
    resolved_at = Column(DateTime, nullable=True)
