import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {


    async createVault(vaultData) {
        if (vaultData.isPrivate != true) {
            vaultData.isPrivate = false
        }
        const res = await api.post('api/vaults', vaultData)
        logger.log(res.data)
        AppState.myVaults.push(res.data)
    }
}

export const vaultsService = new VaultsService()