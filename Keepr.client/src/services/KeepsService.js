import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

    async getAllKeeps(query = {}) {
        const res = await api.get('api/keeps/', { params: query })
        logger.log(res.data)
        AppState.keeps = res.data
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log(res.data)
        AppState.keeps.push(res.data)
    }

    async setActive(id) {
        const res = await api.get('api/keeps/' + id)
        logger.log(res.data)
        res.data.views += 1
        AppState.activeKeep = res.data

    }


    async searchKeep(query = '') {
        const res = await api.get(`api/keeps?query=${query}`)
        logger.log(res.data)
        AppState.keeps = res.data
    }


    async deleteKeep(id) {
        const res = await api.delete('api/keeps/' + id)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }


}


export const keepsService = new KeepsService()