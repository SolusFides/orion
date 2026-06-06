from sqlalchemy import Column, String, JSON, DateTime
from app.database import Base

class Template(Base):
    __tablename__ = "templates"
    
    id = Column(String, primary_key=True, index=True)  # UUID
    name = Column(String, nullable=False)
    config = Column(JSON, nullable=False)
    deleted_at = Column(DateTime, nullable=True)