from fastapi import Depends, HTTPException, status
from app.models.user import User
from app.dependencies.auth import token_required


async def admin_required(user: User = Depends(token_required)) -> User:
    if user.role != "admin":
        raise HTTPException(
            status_code=status.HTTP_403_FORBIDDEN,
            detail="Access denied: Admin role required",
        )
    return user
