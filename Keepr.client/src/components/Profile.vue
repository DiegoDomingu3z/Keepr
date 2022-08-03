<template>
  <div class="row d-flex m-5">
    <div class="col-md-2 text-center">
      <img
        title="Profile Img"
        class="profile-img"
        :src="profile.picture"
        alt=""
      />
    </div>
    <div class="col-md-10 ps-0 header" v-if="account.id == profile.id">
      <p class="f-20">
        <b>{{ profile.name }}</b>
      </p>
      <p class="f-20">
        <b>Vaults: {{ myVaults.length }}</b>
      </p>
      <p class="f-20">
        <b>Keeps: {{ keep.length }}</b>
      </p>
    </div>
    <div class="col-md-10 ps-0 header" v-else>
      <p class="f-20">
        <b>{{ profile.name }}</b>
      </p>
      <p class="f-20">
        <b>Vaults: {{ vault.length }}</b>
      </p>
      <p class="f-20">
        <b>Keeps: {{ keep.length }}</b>
      </p>
    </div>
  </div>
  <div class="row px-5 pt-5 mx-4 mb-3 create">
    <div class="d-flex">
      <h1>
        Vaults
        <i
          v-if="account.id == profile.id"
          title="Create Vault"
          class="mdi mdi-plus text-info"
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#vault"
        ></i>
      </h1>
    </div>
  </div>
  <div class="media-scroller" v-if="account.id == profile.id">
    <div
      class="my-2 d-flex justify-content-evenly vault-img pictures mx-3"
      v-for="v in myVaults"
      :key="v.img"
    >
      <img
        title="Go To Vault"
        @click="goToVault(v.id)"
        class="img-fluid rounded selectable elevation-2"
        :src="v.img"
        alt=""
      />
      <div class="vault-name text-start fs-5 text-white">{{ v.name }}</div>
    </div>
  </div>
  <div class="media-scroller" v-else>
    <div
      class="my-2 d-flex justify-content-evenly vault-img pictures mx-3"
      v-for="v in vault"
      :key="v.img"
    >
      <img
        title="Go To Vault"
        @click="goToVault(v.id)"
        class="img-fluid vault-img selectable elevation-2"
        :src="v.img"
        alt=""
      />
    </div>
  </div>
  <div class="row px-5 mx-4 pt-5 mb-3 create">
    <h1>
      Keeps
      <i
        v-if="account.id == profile.id"
        title="Create Keep"
        class="mdi mdi-plus text-info"
        type="button"
        data-bs-toggle="modal"
        data-bs-target="#create-keep"
      ></i>
    </h1>
  </div>
  <div class="masonry-frame pb-4">
    <div class="m-1 mb-3" v-for="k in keep" :key="k.img">
      <div @click="setActive(k.id)" class="vault-img pictures">
        <img
          title="Open Modal"
          class="img-fluid selectable rounded elevation-2"
          :src="k.img"
          alt=""
        />
        <div class="vault-name ms-2 fs-5 text-white">{{ k.name }}</div>
      </div>
    </div>
  </div>
  <VaultModal />
  <CreateKeepModal />

  <Modal id="active-keep">
    <template #body><ActiveKeepModal /> </template>
  </Modal>
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
import { Modal } from 'bootstrap'

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
          router.push({ name: "Home" })
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      async setActive(id) {
        try {
          await keepsService.setActive(id)
          Modal.getOrCreateInstance(document.getElementById("active-keep")).show();
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
@media (min-width: 780px) {
  .media-scroller {
    display: grid;
    gap: var(--size-3);
    grid-auto-flow: column;
    grid-auto-columns: 21%;
    overflow-x: auto;
    overscroll-behavior-inline: contain;
  }
}

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
@media (max-width: 780px) {
  .create {
    padding: 0;
    margin-bottom: 2em;
    padding-left: 0 !important;
  }
}

.vault-img {
  position: relative;
}

.vault-name {
  position: absolute;
  z-index: 100;
  left: 0.7em;
  bottom: 0.2em;
  text-shadow: 2px 2px black;
}

.pictures:hover {
  transform: scale(1.025);
  transition: 300ms;
}
</style>