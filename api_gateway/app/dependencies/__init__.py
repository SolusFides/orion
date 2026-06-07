from typing import Annotated
from fastapi import Depends
from app.models.user import User
from app.dependencies.auth import token_required
from app.dependencies.admin import admin_required

CurrentUserDep = Annotated[User, Depends(token_required)]
AdminDep = Annotated[User, Depends(admin_required)]
