import { useAttrs } from "vue"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

    async getById(id) {
        const res = await api.get('api/vaults/' + id)
        logger.log(res.data)
        AppState.activeVault = res.data
    }
    async createVault(vaultData) {
        if (vaultData.isPrivate != true) {
            vaultData.isPrivate = false
        }
        const res = await api.post('api/vaults', vaultData)
        logger.log(res.data)
        AppState.myVaults.push(res.data)
        AppState.activeProfileVaults.push(res.data)
    }

    async deleteVault(id) {
        const res = await api.delete('api/vaults/' + id)
        logger.log(res.data)
        AppState.myVaults = AppState.myVaults.filter(v => v.id != id)
    }
}

export const vaultsService = new VaultsService()