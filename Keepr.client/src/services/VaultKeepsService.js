import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async getKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log(res.data)
    }
    async create(data) {
        const res = await api.post('api/vaultkeeps', data)
        logger.log(res.data)

    }


}

export const vaultKeepsService = new VaultKeepsService()