from sqlalchemy import Column, String, Integer, ForeignKey
from app.database import Base

class Screen(Base):
    __tablename__ = "screens"
    
    id = Column(String, primary_key=True, index=True)
    name = Column(String, nullable=False)
    complex_id = Column(Integer, nullable=False)
    building_id = Column(Integer, nullable=False)
    entrance = Column(Integer, nullable=True)
    current_template_id = Column(String, ForeignKey("templates.id"), nullable=True)
