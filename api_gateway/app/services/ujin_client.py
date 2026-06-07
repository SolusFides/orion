import logging
from typing import Dict, Any, List, Optional
import httpx
from app.config import settings

logger = logging.getLogger(__name__)

class UjinClient:
    def __init__(self) -> None:
        self.base_url: str = settings.UJIN_BASE_URL.rstrip('/')
        self.token: str = settings.UJIN_TOKEN
        self.timeout: float = 10.0

    async def _request(self, method: str, path: str, params: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
        if params is None:
            params = {}
        params["token"] = self.token
        
        url = f"{self.base_url}/{path.lstrip('/')}"
        
        async with httpx.AsyncClient(timeout=self.timeout) as client:
            try:
                response = await client.request(method, url, params=params)
                response.raise_for_status()
                data = response.json()
                
                if data.get("error", 0) != 0:
                    error_msg = data.get("message") or data.get("error_code") or "Unknown Ujin API error"
                    logger.error(f"Ujin API error for {path}: {error_msg}")
                    raise httpx.HTTPStatusError(
                        message=error_msg,
                        request=response.request,
                        response=response
                    )
                return data
            except httpx.HTTPError as exc:
                logger.error(f"HTTP exception during Ujin API request to {url}: {exc}")
                raise

    async def get_complexes(self) -> List[Dict[str, Any]]:
        data = await self._request("GET", "/v1/complex/list/")
        return data.get("data", {}).get("items", [])

    async def get_buildings(self, complex_id: Optional[int] = None) -> List[Dict[str, Any]]:
        params = {}
        if complex_id is not None:
            params["complex_id"] = complex_id
        data = await self._request("GET", "/v1/buildings/get-list-crm/", params=params)
        return data.get("data", {}).get("buildings", [])

    async def get_free_parking(self, complex_id: int, building_id: int) -> List[Dict[str, Any]]:
        params = {
            "complexes[]": complex_id,
            "buildings[]": building_id
        }
        data = await self._request("GET", "/api/v1/parking/free", params=params)
        return data.get("data", {}).get("items", [])

    async def get_free_storage(self, complex_id: int, building_id: int) -> List[Dict[str, Any]]:
        params = {
            "complexes[]": complex_id,
            "buildings[]": building_id
        }
        data = await self._request("GET", "/api/v1/storage/unassigned", params=params)
        return data.get("data", {}).get("items", [])

    async def get_news(self, complex_id: Optional[int] = None, building_id: Optional[int] = None) -> List[Dict[str, Any]]:
        params: Dict[str, Any] = {"type": "news"}
        if complex_id is not None:
            params["complexes[]"] = complex_id
        if building_id is not None:
            params["buildings[]"] = building_id
        data = await self._request("GET", "/v1/news/list", params=params)
        return data.get("data", {}).get("items", [])

    async def get_news_detail(self, news_id: int) -> Dict[str, Any]:
        params = {"id": news_id}
        data = await self._request("GET", "/v1/news/view", params=params)
        return data.get("data", {}).get("item", {})
