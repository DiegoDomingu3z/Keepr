import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {

    async getUserProf(id) {
        const res = await api.get('api/profiles/' + id)
        logger.log(res.data)
        AppState.activeProfile = res.data
    }

    async getUserKeeps(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        logger.log(res.data)
        AppState.activeProfileKeeps = res.data
    }

    async getUserVaults(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log(res.data)
        AppState.activeProfileVaults = res.data

    }
}


export const profilesService = new ProfilesService()