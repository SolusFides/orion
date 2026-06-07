from typing import List, Dict, Any, Optional
from fastapi import APIRouter, HTTPException, status
import httpx
from app.services.ujin_client import UjinClient

router = APIRouter(prefix="/api/ujin", tags=["Ujin Proxy"])
ujin_client = UjinClient()

@router.get("/complexes", response_model=List[Dict[str, Any]])
async def get_complexes():
    try:
        return await ujin_client.get_complexes()
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )

@router.get("/buildings", response_model=List[Dict[str, Any]])
async def get_buildings(complex_id: Optional[int] = None):
    try:
        return await ujin_client.get_buildings(complex_id)
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )

@router.get("/parking/free", response_model=List[Dict[str, Any]])
async def get_free_parking(complex_id: int, building_id: int):
    try:
        return await ujin_client.get_free_parking(complex_id, building_id)
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )

@router.get("/storage/free", response_model=List[Dict[str, Any]])
async def get_free_storage(complex_id: int, building_id: int):
    try:
        return await ujin_client.get_free_storage(complex_id, building_id)
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )

@router.get("/news", response_model=List[Dict[str, Any]])
async def get_news(complex_id: Optional[int] = None, building_id: Optional[int] = None):
    try:
        return await ujin_client.get_news(complex_id, building_id)
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )

@router.get("/news/{news_id}", response_model=Dict[str, Any])
async def get_news_detail(news_id: int):
    try:
        return await ujin_client.get_news_detail(news_id)
    except httpx.HTTPError as exc:
        raise HTTPException(
            status_code=status.HTTP_502_BAD_GATEWAY,
            detail=f"Error communicating with Ujin API: {str(exc)}"
        )
