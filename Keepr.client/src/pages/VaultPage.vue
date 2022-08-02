<template>
  <div class="component"></div>
</template>


<script>
import { onMounted } from '@vue/runtime-core'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute } from 'vue-router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultKeepsService.getKeeps(route.params.id)
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