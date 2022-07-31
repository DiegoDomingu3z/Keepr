<template>
  <div class="container">
    <div class="masonry-frame">
      <div v-for="k in keep" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.log(error)
        Pop.toast(error.Message)
      }
    })
    return {
      keep: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
  columns: 4;
  div {
    break-inside: avoid;
  }
}
</style>
