<template>
  <nav class="navbar navbar-expand-lg bg-info px-3 justify-content-between">
    <div>
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <h1 class="title">KeepR.</h1>
      </router-link>
    </div>
    <!-- <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button> -->
    <form @submit.prevent="searchKeep" id="search" action="">
      <div class="text-center input-group">
        <input
          type="text"
          class="form-control input bg-white"
          placeholder="Search..."
          v-model="search"
        />
        <button type="submit" class="btn bg-white border">
          <i class="mdi mdi-magnify"></i>
        </button>
      </div>
    </form>
    <div>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { ref } from '@vue/reactivity';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
export default {
  setup() {
    const search = ref("")
    return {
      search,
      async searchKeep() {
        try {
          await keepsService.searchKeep(search.value)
          document.getElementById("search").reset();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      }

    };
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.title {
  color: #636e72;
}

.input {
  width: 400px;
}

@media (max-width: 768px) {
  .input {
    width: auto;
  }
}
</style>
