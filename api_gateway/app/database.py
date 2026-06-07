import logging
import sys
import traceback
from contextlib import asynccontextmanager

from sqlalchemy.exc import SQLAlchemyError
from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession
from sqlalchemy.orm import sessionmaker, declarative_base
from app.config import settings

engine = create_async_engine(settings.DATABASE_URL, echo=False)

AsyncSessionLocal = sessionmaker(
    bind=engine,
    class_=AsyncSession,
    expire_on_commit=False
)

Base = declarative_base()


@asynccontextmanager
async def session_manager() -> AsyncSession:
    try:
        async with AsyncSessionLocal() as session:
            yield session
    except SQLAlchemyError as ex:
        exc_type, exc_value, exc_traceback = sys.exc_info()
        formatted = traceback.format_exception(exc_type, exc_value, exc_traceback)
        logging.error("%s\n%s" % (str(ex), "\n".join(formatted)))
        raise
