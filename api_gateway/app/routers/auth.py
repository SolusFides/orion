from fastapi import APIRouter, Depends, HTTPException, status
from app.schemas.auth import LoginRequest, LoginResponse
from app.repositories.user import UserRepository
from app.dependencies.auth import create_access_token, oauth2_scheme, token_required
from app.dependencies.blacklist import BLACKLIST_TOKENS
from app.config import settings
from app.models.user import User

import bcrypt

router = APIRouter(prefix="/api/auth", tags=["Auth"])
user_repo = UserRepository()

@router.post("/login", response_model=LoginResponse)
async def login(login_data: LoginRequest = Depends(LoginRequest.as_form)):
    user = await user_repo.get_by_username(login_data.username)
    
    if not user or not bcrypt.checkpw(login_data.password.encode("utf-8"), user.password_hash.encode("utf-8")):
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Incorrect username or password",
            headers={"WWW-Authenticate": "Bearer"},
        )
    
    access_token = create_access_token(data={"sub": user.username})
    return LoginResponse(
        access_token=access_token,
        token_type="bearer",
        expires_in=settings.ACCESS_TOKEN_EXPIRE
    )

@router.post("/logout")
async def logout(
    token: str = Depends(oauth2_scheme), 
    current_user: User = Depends(token_required)
):
    BLACKLIST_TOKENS.add(token)
    return {"message": "Successfully logged out"}
