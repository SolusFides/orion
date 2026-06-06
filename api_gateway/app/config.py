from pydantic_settings import BaseSettings
from pydantic import ConfigDict

class Settings(BaseSettings):
    model_config = ConfigDict(env_file=".env", env_file_encoding="utf-8", extra="ignore")
    
    UJIN_TOKEN: str
    UJIN_BASE_URL: str = "https://hck-api.unicorn.icu"
    
    SECRET_KEY: str
    ACCESS_TOKEN_EXPIRE: int = 86400
    
    ADMIN_USERNAME: str = "admin"
    ADMIN_PASSWORD: str = "admin123"
    
    DATABASE_URL: str = "sqlite+aiosqlite:////app/data/orion.db"

settings = Settings()
