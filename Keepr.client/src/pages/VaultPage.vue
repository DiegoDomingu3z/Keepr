<template>
  <div class="container">
    <VaultKeeps />
  </div>
</template>


<script>
import { onMounted } from '@vue/runtime-core'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultKeepsService.getKeeps(route.params.id)
        await vaultsService.getById(route.params.id)
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
</style>