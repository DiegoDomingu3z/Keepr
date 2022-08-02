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
      <h1>
        Vaults
        <i
          v-if="account.id == profile.id"
          class="mdi mdi-plus text-info"
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#vault"
        ></i>
      </h1>
    </div>
  </div>
  <div class="row" v-if="account.id == profile.id">
    <div
      class="col-md-2 my-2 d-flex justify-content-evenly vault-img"
      v-for="v in myVaults"
      :key="v.img"
    >
      <img
        @click="goToVault(v.id)"
        class="img-fluid rounded"
        :src="v.img"
        alt=""
      />
      <div class="vault-name text-start fs-5 text-white">{{ v.name }}</div>
    </div>
  </div>
  <div class="row" v-else>
    <div
      class="col-md-2 my-2 d-flex justify-content-evenly"
      v-for="v in vault"
      :key="v.img"
    >
      <img class="img-fluid vault-img" :src="v.img" alt="" />
    </div>
  </div>
  <div class="row px-5 mx-4 pt-5">
    <h1>
      Keeps
      <i
        class="mdi mdi-plus text-info"
        type="button"
        data-bs-toggle="modal"
        data-bs-target="#create-keep"
      ></i>
    </h1>
  </div>
  <div class="masonry-frame pb-4">
    <div class="m-2" v-for="k in keep" :key="k.img">
      <div class="vault-img">
        <img class="img-fluid selectable rounded" :src="k.img" alt="" />
        <div class="vault-name ms-2 fs-5 text-white">{{ k.name }}</div>
      </div>
    </div>
  </div>
  <VaultModal />
  <CreateKeepModal />
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'

export default {

  setup() {
    const route = useRoute()
    const router = useRouter()
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
      async goToVault(id) {
        try {
          await vaultsService.getById(id)
          router.push({ name: "Vault", params: { id: this.activeVault.id } })
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },

      profile: computed(() => AppState.activeProfile),
      vault: computed(() => AppState.activeProfileVaults),
      keep: computed(() => AppState.activeProfileKeeps),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      activeVault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
  columns: 6 200px;

  div {
    break-inside: avoid;
  }
}
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

.vault-img {
  position: relative;
}

.vault-name {
  position: absolute;
  z-index: 100;
  left: 0.5em;
  bottom: 0.2em;
}

.keep-img {
  height: 200px;
  width: 200px;
  background-position: center;
  background-size: cover;
  object-fit: cover;
}
</style>