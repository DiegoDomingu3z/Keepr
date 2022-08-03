import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async getKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log(res.data)
        AppState.activeVaultKeeps = res.data
        logger.log(AppState.activeProfileVaults, "[AppState]")
    }
    async create(data) {
        const res = await api.post('api/vaultkeeps', data)
        logger.log(res.data)
        AppState.activeKeep.kept++
        AppState.activeVaultKeeps.push(res.data)
    }

    async deleteVaultKeep(id) {
        const res = await api.delete('api/vaultkeeps/' + id)
        logger.log(res.data)
        AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(v => v.vaultKeepId != id)

    }




}

export const vaultKeepsService = new VaultKeepsService()