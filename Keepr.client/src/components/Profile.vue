<template>
  <div class="row d-flex m-5">
    <div class="col-md-2 text-center">
      <img class="profile-img" :src="profile.picture" alt="" />
    </div>
    <div class="col-md-10 ps-0 header" v-if="account.id == profile.id">
      <h3>{{ profile.name }}</h3>
      <h5>Vaults: {{ myVaults.length }}</h5>
      <h5>Keeps: {{ keep.length }}</h5>
    </div>
    <div class="col-md-10 ps-0 header" v-else>
      <h3>{{ profile.name }}</h3>
      <h5>Vaults: {{ vault.length }}</h5>
      <h5>Keeps: {{ keep.length }}</h5>
    </div>
  </div>
  <div class="row px-5 pt-5 mx-4">
    <div class="d-flex">
      <h2>
        Vaults
        <i
          v-if="account.id == profile.id"
          class="mdi mdi-plus text-info"
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#vault"
        ></i>
      </h2>
    </div>
  </div>
  <div>
    <img v-for="v in myVaults" :key="v.img" :src="v.img" alt="" />
  </div>
  <div class="row px-5 mx-4 pt-5"><h1>Keeps</h1></div>
  <VaultModal />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { accountService } from '../services/AccountService'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getUserProf(route.params.id)
        await profilesService.getUserKeeps(route.params.id)
        await profilesService.getUserVaults(route.params.id)

      } catch (error) {
        logger.log(error)
        Pop.toast(error.message)
      }
    })
    return {

      profile: computed(() => AppState.activeProfile),
      vault: computed(() => AppState.activeProfileVaults),
      keep: computed(() => AppState.activeProfileKeeps),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 150px;
  width: 150px;
}

@media (max-width: 780px) {
  .header {
    text-align: center;
    padding-top: 1em;
  }
}
</style>